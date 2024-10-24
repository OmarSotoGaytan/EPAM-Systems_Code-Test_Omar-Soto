interface InputTextProps {
    value: string;
    disabled: boolean;
    placeholder: string;
    onChange: (value: string) => void;
}

export const InputText = ({ value, disabled, placeholder, onChange }: InputTextProps) => {
    return (
        <div>
            <div className="mt-2">
                <input
                    type="text"
                    value={value}
                    disabled={disabled}
                    placeholder={placeholder}
                    onChange={(e) => onChange(e.target.value)}
                    className="block outline-none border-none w-full min-w-80 p-4 rounded-md border-0 py-1.5 text-black-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-white-400 focus:ring-2 focus:ring-inset focus:ring-green-900 sm:text-md sm:leading-6"
                />
            </div>
        </div>
    );
};
