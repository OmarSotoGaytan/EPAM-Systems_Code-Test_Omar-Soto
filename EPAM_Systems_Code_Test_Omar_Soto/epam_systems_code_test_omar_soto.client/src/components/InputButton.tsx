import { ReactNode } from "react";

interface InputButtonProps {
    onClick: () => void;
    disabled?: boolean;
    children: ReactNode;
}

export const InputButton = ({ onClick, disabled = false, children }: InputButtonProps) => {
    return (
        <button onClick={onClick} disabled={disabled} >
            {children}
        </button>
    );
};
