import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { toast } from "react-toastify";
import { getBasicAuthToken } from "../auth";
import { HubReceivers, HubSenders, MainHubs } from "../HubNames";
import { TextProcessorResult } from "../entities";

interface UseTextProcessorHub {
    output: string;
    progressValue: number;
    processing: boolean;
    startProcess: (input: string) => void;
    cancelProcess: () => void;
}

const RECONNECT_TIMEOUT_MS = 60000;
const MAX_RETRY_DELAY_MS = 10000;

export const useTextProcessorHub = (): UseTextProcessorHub => {
    const [output, setOutput] = useState('');
    const [processing, setProcessing] = useState(false);
    const [progressValue, setProgressValue] = useState(0);

    const connectionRef = useRef<HubConnection | null>(null);

    const reset = () => {
        setProcessing(false);
        setOutput('');
        setProgressValue(0);
    };

    const startProcess = (input: string) => {
        setProcessing(true);
        setOutput('');
        setProgressValue(0);

        connectionRef.current?.invoke(HubSenders.PROCESS_TEXT, connectionRef.current.connectionId, input);
        toast.success('Process has been started.');
    };

    const cancelProcess = () => {
        connectionRef.current?.invoke(HubSenders.CANCEL_PROCESS, connectionRef.current.connectionId);
        setProcessing(false);
    };

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(import.meta.env.VITE_SIGNALR_URL + MainHubs.TEXT_PROCESSOR, {
                headers: {
                    'Authorization': `Basic ${getBasicAuthToken()}`
                }
            }).withAutomaticReconnect({
                nextRetryDelayInMilliseconds: retryContext => {
                    if (retryContext.elapsedMilliseconds < RECONNECT_TIMEOUT_MS) { 
                        // If we've been reconnecting for less than RECONNECT_TIMEOUT_MS seconds so far,
                        // wait between 0 and MAX_RETRY_DELAY_MS seconds before the next reconnect attempt.
                        return Math.random() * MAX_RETRY_DELAY_MS;
                    } else {
                        // If we've been reconnecting for more than RECONNECT_TIMEOUT_MS seconds so far, stop reconnecting.
                        return null;
                    }
                }
            })
            .build();

        connection.on(HubReceivers.RECEIVE_PROGRESS, (result: TextProcessorResult) => {
            setOutput((prev) => prev + result.currentChar);
            setProgressValue(result.progress);
        });

        connection.on(HubReceivers.PROCESS_CANCELLED, () => {
            toast.error('Process has been cancelled.');
            reset();
        });

        connection.on(HubReceivers.PROCESS_COMPLETED, () => {
            toast.success('Process has been completed.');
            setProcessing(false);
        });

        connection.start().then(() => {
            toast.success('Conected Succesfully.');
            connectionRef.current = connection;
        });

        const start = async () => {
            try {
                await connection.start();
                toast.success('Conected Succesfully.');
            } catch (err) {
                setTimeout(start, 5000);
            }
        };

        connection.onclose(async () => {
            await start();
        });

        connection.onreconnecting((error) => {
            toast.warn(`Connection lost due to error "${error}". Reconnecting.`);
        });

        return () => {
            connection.stop();
            reset();
        };
    }, []);

    return {
        output,
        progressValue,
        processing,
        startProcess,
        cancelProcess
    };
};
