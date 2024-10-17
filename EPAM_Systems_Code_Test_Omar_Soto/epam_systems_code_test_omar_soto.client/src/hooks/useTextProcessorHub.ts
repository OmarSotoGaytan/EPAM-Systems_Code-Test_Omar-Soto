import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { HubReceivers, HubSenders } from "../HubNames";
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
        connectionRef.current?.invoke(HubSenders.PROCESS_TEXT, connectionRef.current.connectionId, input);
    };

    const cancelProcess = () => {
        connectionRef.current?.invoke(HubSenders.CANCEL_PROCESS, connectionRef.current.connectionId);
        setProcessing(false);
    };

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:7048/textprocessinghub')
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
