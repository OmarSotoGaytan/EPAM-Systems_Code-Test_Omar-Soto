import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";

export const TextProcessorPage = () => {
    const connectionRef = useRef<HubConnection | null>(null);

    const [input, setInput] = useState('');
    const [output, setOutput] = useState('');
    const [processing, setProcessing] = useState(false);

    const startProcess = () => {
        setProcessing(true);
        setOutput('');
        connectionRef.current?.invoke('ProcessText', connectionRef.current.connectionId, input);
    };

    const cancelProcess = () => {
        connectionRef.current?.invoke('CancelProcess', connectionRef.current.connectionId);
        setProcessing(false);
    };

    const reset = () => {
        setProcessing(false);
        setOutput('');
        setInput('');
    };

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:7048/textprocessinghub')
            .build();

        connection.on('ReceiveCharacters', (char) => {
            setOutput((prev) => prev + char);
        });

        connection.on('ProcessCancelled', () => {
            reset();
        });

        connection.on('ProcessCompleted', () => {
            setProcessing(false);
            setInput('');
        });

        connection.start().then(() => {
            connectionRef.current = connection;
        });

        return () => {
            connection.stop();
            reset();
        };
    }, []);

    return (
        <div className="w-full flex flex-col items-center justify-center gap-5">
            <h1 className="text-2xl font-bold underline">
                EPAM Systems - Code Test - Omar Soto
            </h1>
            <textarea
                value={input}
                onChange={(e) => setInput(e.target.value)}
                disabled={processing}               
                placeholder="Enter your text here"
            />
            <div className="flex gap-4">
                <button onClick={startProcess} disabled={processing || !input}>
                    Process
                </button>
                <button onClick={cancelProcess}>
                    Cancel
                </button>
            </div>
            <div>
                <span>Output:</span>
                <span>{output}</span>
            </div>
        </div>
    );
};
