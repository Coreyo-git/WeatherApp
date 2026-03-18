import { AlertTriangle } from "lucide-react";

interface Props {
    message: string;
}

export function ErrorMessage({ message }: Props) {
    return (
        <div className="flex items-center gap-3 mt-6 p-4 rounded-lg bg-red-900/60 border border-red-500 text-red-100">
            <AlertTriangle size={24} className="shrink-0" />
            <p>{message}</p>
        </div>
    );
}
