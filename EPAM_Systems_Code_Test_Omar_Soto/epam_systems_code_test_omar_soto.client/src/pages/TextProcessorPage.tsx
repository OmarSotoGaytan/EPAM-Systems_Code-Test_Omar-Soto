import { useState } from "react";
import { useTextProcessorHub } from "../hooks/useTextProcessorHub";

export const TextProcessorPage = () => {
    const { output, processing, startProcess, cancelProcess } = useTextProcessorHub();
    const [input, setInput] = useState('');

    const reset = () => {
        cancelProcess();
        setInput('');
    };

    return (
        <div className="w-full flex flex-col items-center justify-center gap-5">
            <h1 className="text-2xl font-bold underline">
                EPAM Systems - Code Test - Omar Soto
            </h1>
            <input
                type="text"
                value={input}
                onChange={(e) => setInput(e.target.value)}
                disabled={processing}
                placeholder="Enter your text here"
            />
            <div className="flex gap-4">
                <button onClick={() => startProcess(input)} disabled={processing || !input}>
                    Process
                </button>
                <button onClick={reset}>
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
