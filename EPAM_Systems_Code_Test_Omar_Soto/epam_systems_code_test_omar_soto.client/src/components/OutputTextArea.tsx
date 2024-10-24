interface OutputTextareaProps {
    value: string;
    label: string;
}

export const OutputTextarea = ({ value, label, }: OutputTextareaProps) => {
    return (
        <div>
            <label
                htmlFor="output"
                className="w-full min-w-80 block text-sm font-medium leading-6 text-white text-left"
            >
                {label}:
            </label>
            <div className="mt-2">
                <textarea
                    id="output"
                    name="output"
                    rows={2}
                    value={value}
                    readOnly
                    className="p-4 block outline-none border-none resize-none w-full rounded-md py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-green-700 sm:leading-6"
                />
            </div>
        </div>
    );
};