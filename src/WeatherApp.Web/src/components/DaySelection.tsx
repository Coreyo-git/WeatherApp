import type { ForecastDay } from "../types/weather";

interface Props {
    days: ForecastDay[];
    selectedDate: string;
    onSelect: (date: string) => void;
}

export function DaySelection({ days, selectedDate, onSelect }: Props) {
    return (
        <nav className="flex gap-2 py-3">
            {days.map((day) => {
                const label = new Date(day.date + "T00:00:00").toLocaleDateString("en-US", {
                    weekday: "short",
                    month: "short",
                    day: "numeric",
                });
                const isSelected = day.date === selectedDate;

                return (
                    <button
                        key={day.date}
                        onClick={() => onSelect(day.date)}
                        aria-pressed={isSelected}
                        className={`flex flex-1 flex-col items-center gap-1 rounded-xl px-2 py-3 text-sm transition-colors cursor-pointer
                            ${isSelected
                                ? "bg-black/90 ring-2 ring-white/60"
                                : "bg-black/80 hover:bg-black/95"
                            }`}
                    >
                        <span className="font-semibold">{label}</span>
                        <img
                            src={`https:${day.summary.condition.iconUrl}`}
                            alt={day.summary.condition.text}
                            className="h-10 w-10"
                        />
                        <span>{day.summary.maxTemperatureCelsius}°C</span>
                    </button>
                );
            })}
        </nav>
    );
}
