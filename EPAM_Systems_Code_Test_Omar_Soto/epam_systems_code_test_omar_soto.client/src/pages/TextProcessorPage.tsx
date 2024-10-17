import { useEffect, useState } from "react";
import { InputButton } from "../components/InputButton";
import { useTextProcessorHub } from "../hooks/useTextProcessorHub";
import { InputText } from "../components/InputText";

export const TextProcessorPage = () => {
    const { output, processing, startProcess, cancelProcess } = useTextProcessorHub();

    const [input, setInput] = useState('');

    const reset = () => {
        cancelProcess();
        setInput('');
    };

    useEffect(() => {
        if (!processing) {
            setInput('');
        }
    }, [processing]);

    return (
        <div className="w-full flex flex-col items-center justify-center gap-5">
            <h1 className="text-2xl font-bold underline">
                EPAM Systems - Code Test - Omar Soto
            </h1>
            <InputText
                value={input}
                onChange={(val) => setInput(val)}
                disabled={processing}
                placeholder="Enter your text here"
            />
            <div className="flex gap-4">
                <InputButton
                    onClick={() => startProcess(input)}
                    disabled={processing || !input}
                >
                    Process
                </InputButton>
                <InputButton onClick={reset} disabled={!processing}>
                    Cancel
                </InputButton>
            </div>
            <div>
                <span>Output:</span>
                <span>{output}</span>
            </div>
        </div>
    );
};
