interface ProgressBarProps {
    min: number;
    max: number;
    progressValue: number;
}

export const ProgressBar = ({ min, max, progressValue }: ProgressBarProps) => {
    return (
        <>
            <div
                className="flex w-80 h-8 bg-gray-200 rounded-full overflow-hidden dark:bg-neutral-700"
                role="progressbar"
                aria-valuenow={progressValue}
                aria-valuemin={min}
                aria-valuemax={max}
            >
                <div
                    className="flex flex-col justify-center rounded-full overflow-hidden bg-green-600 text-xs text-white text-center whitespace-nowrap dark:bg-blue-500 transition duration-500"
                    style={{ width: `${progressValue}%` }}
                >
                    {progressValue}%
                </div>
            </div>
        </>
    )
};
