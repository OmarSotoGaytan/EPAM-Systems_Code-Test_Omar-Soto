import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";

export const TextProcessorPage = () => {
    const [input, setInput] = useState("");
    const [result, setResult] = useState("");
    const [processing, setProcessing] = useState(false);
    const connection = useRef<HubConnection | null>(null);

    const startProcessing = async () => {
        if (!connection.current) return;
        setProcessing(true);
        setResult("");

        connection.current.on("ReceiveCharacter", (char: string) => {
            console.log(char);
            setResult((prev) => prev + char);
        });

        await connection.current.invoke("ProcessText", connection.current.connectionId, input);
        setProcessing(false);
    };

    const cancelProcessing = () => {
        connection.current?.stop();
        setProcessing(false);
    };

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl("https://localhost:7048/process")
            .withAutomaticReconnect()
            .build();

        newConnection.start().then(() => {
            console.log("Connected to SignalR Hub");
        });

        connection.current = newConnection;

        return () => {
            connection.current?.stop();
        };
    }, []);

    return (
        <div>
            <h1>Long-Running Job</h1>
            <input
                type="text"
                value={input}
                onChange={(e) => setInput(e.target.value)}
                disabled={processing}
            />
            <button onClick={startProcessing} disabled={processing || !input}>
                Process
            </button>
            <button onClick={cancelProcessing} disabled={!processing}>
                Cancel
            </button>
            <textarea value={result} readOnly rows={10} />
        </div>
    );
};

