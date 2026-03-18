import {
    CloudRain,
    Snowflake,
    Wind,
    Droplets,
    Eye,
    Sun,
    Sunrise,
    Sunset,
    ThermometerSun,
} from "lucide-react";
import type { ForecastDay } from "../types/weather";
import { formatTimeOnly } from "../utils/time";

interface Props {
    today: ForecastDay;
}

interface StatProps {
    icon: React.ReactNode;
    label: string;
    value: string;
}

function Stat({ icon, label, value }: StatProps) {
    return (
        <div className="flex flex-col items-center gap-1 rounded-xl bg-black/80 px-3 py-3 text-sm">
            <span className="text-white/60">{icon}</span>
            <span className="text-white/60 text-xs">{label}</span>
            <span className="font-semibold">{value}</span>
        </div>
    );
}

//TODO support Fahrenheit 
export function ForecastSummary({ today }: Props) {
    const { summary, sunrise, sunset } = today;

    return (
        <div className="rounded-xl bg-black/80 p-4">
            {/* Condition banner */}
            <div className="flex items-center gap-4 mb-4">
                <img
                    src={`https:${summary.condition.iconUrl}`}
                    alt={summary.condition.text}
                    className="h-16 w-16"
                />
                <div>
                    <p className="text-lg font-semibold">{summary.condition.text}</p>
                    <p className="text-white/70 text-sm">
                        High: {summary.maxTemperatureCelsius}°C &nbsp;·&nbsp; Low: {summary.minTemperatureCelsius}°C
                    </p>
                </div>
            </div>

            {/* Stats grid */}
            <div className="grid grid-cols-4 gap-2">
                <Stat
                    icon={<ThermometerSun size={18} />}
                    label="Avg Temp"
                    value={`${summary.avgTemperatureCelsius}°C`}
                />
                <Stat
                    icon={<CloudRain size={18} />}
                    label="Rain"
                    value={`${summary.chanceOfRain}%`}
                />
                {summary.chanceOfSnow > 0 && (
                    <Stat
                        icon={<Snowflake size={18} />}
                        label="Snow"
                        value={`${summary.chanceOfSnow}%`}
                    />
                )}
                <Stat
                    icon={<Wind size={18} />}
                    label="Max Wind"
                    value={`${summary.maxWindKph} kph`}
                />
                <Stat
                    icon={<Droplets size={18} />}
                    label="Humidity"
                    value={`${summary.avgHumidity}%`}
                />
                <Stat
                    icon={<Eye size={18} />}
                    label="Visibility"
                    value={`${summary.avgVisibilityKm} km`}
                />
                <Stat
                    icon={<Sun size={18} />}
                    label="UV Index"
                    value={`${summary.uvIndex}`}
                />
                <Stat
                    icon={<Sunrise size={18} />}
                    label="Sunrise"
                    value={formatTimeOnly(sunrise)}
                />
                <Stat
                    icon={<Sunset size={18} />}
                    label="Sunset"
                    value={formatTimeOnly(sunset)}
                />
            </div>
        </div>
    );
}
