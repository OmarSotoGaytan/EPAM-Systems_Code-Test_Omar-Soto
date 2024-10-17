interface InputTextProps {
    value: string;
    disabled: boolean;
    placeholder: string;
    onChange: (value: string) => void;
}

export const InputText = ({ value, disabled, placeholder, onChange }: InputTextProps) => {
    return (
        <input
            type="text"
            value={value}
            disabled={disabled}
            placeholder={placeholder}
            onChange={(e) => onChange(e.target.value)}
        />
    );
};
