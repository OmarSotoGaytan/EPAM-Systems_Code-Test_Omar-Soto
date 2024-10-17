import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { HubReceivers, HubSenders, MainHubs } from "../HubNames";
import { toast } from "react-toastify";

interface UseTextProcessorHub {
    output: string;
    processing: boolean;
    startProcess: (input: string) => void;
    cancelProcess: () => void;
}

export const useTextProcessorHub = (): UseTextProcessorHub => {
    const [output, setOutput] = useState('');
    const [processing, setProcessing] = useState(false);
    const connectionRef = useRef<HubConnection | null>(null);

    const reset = () => {
        setProcessing(false);
        setOutput('');
    };

    const startProcess = (input: string) => {
        setProcessing(true);
        setOutput('');
        toast.success('Process has been started.');
        connectionRef.current?.invoke(HubSenders.PROCESS_TEXT, connectionRef.current.connectionId, input);
    };

    const cancelProcess = () => {
        connectionRef.current?.invoke(HubSenders.CANCEL_PROCESS, connectionRef.current.connectionId);
        setProcessing(false);
    };

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(import.meta.env.VITE_SIGNALR_URL + MainHubs.TEXT_PROCESSOR)
            .build();

        connection.on(HubReceivers.RECEIVE_CHAR, (char) => {
            setOutput((prev) => prev + char);
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

        return () => {
            connection.stop();
            reset();
        };
    }, []);

    return {
        output,
        processing,
        startProcess,
        cancelProcess
    };
};
