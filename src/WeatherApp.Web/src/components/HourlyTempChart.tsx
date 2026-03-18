import { AreaChart, XAxis, YAxis, Area, ResponsiveContainer, Tooltip, CartesianGrid } from "recharts";
import type { WeatherSnapshot } from "../types/weather";
import { formatHour } from "../utils/time";

interface Props {
    hours: WeatherSnapshot[];
}

export function HourlyTempChart({ hours }: Props) {
    // Nothing to show if the API didn't return hourly data for this day
    if (hours.length === 0) return null;

    // Recharts wants a plain array of objects — pull out just what the chart needs
    const data = hours.map((h) => ({
        time: new Date(h.time).getHours(),
        temp: h.temperatureCelsius,
    }));

    // Define min (bottom) max (top) bounds for graph
    const temps = data.map(d => d.temp);
    const min = Math.floor(Math.min(...temps));
    const max = Math.ceil(Math.max(...temps));

    return (
        <div className="rounded-xl bg-black/80 p-4 flex-1">
            <p className="text-sm text-white/60 mb-3 font-semibold">Hourly Temperature</p>
            <ResponsiveContainer width="100%" height={228}>
                <AreaChart data={data} margin={{ top: 4, right: 8, bottom: 0, left: -8 }}>
                    <defs>
                        {/* Fade the fill out toward the bottom so it doesn't look too heavy */}
                        <linearGradient id="tempGrad" x1="0" y1="0" x2="0" y2="1">
                            <stop offset="5%" stopColor="#60a5fa" stopOpacity={0.3} />
                            <stop offset="95%" stopColor="#60a5fa" stopOpacity={0} />
                        </linearGradient>
                    </defs>

                    {/* light grid to help data easier to track */}
                    <CartesianGrid strokeDasharray="3 3" stroke="rgba(255,255,255,0.06)" />

                    {/* Shows every other hour */}
                    <XAxis
                        dataKey="time"
                        tickFormatter={formatHour}
                        tick={{ fill: "rgba(255,255,255,0.4)", fontSize: 13 }}
                        tickLine={false}
                        axisLine={false}
                        interval={1} // skips 1 tick between each point, 1, 3, 5, 6pm etc
                    />

                    {/* Y-axis range is padded so the curve has breathing room */}
                    <YAxis
                        domain={[min, max]}
                        tickFormatter={v => `${v}°`}
                        tick={{ fill: "rgba(255,255,255,0.4)", fontSize: 13 }}
                        tickLine={false}
                        axisLine={false}
                        width={50}
                    />

                    {/* Hover tooltip — recharts types the label as ReactNode so we cast to number */}
                    <Tooltip
                        formatter={(value) => [`${value}°C`, "Temp"]}
                        labelFormatter={(label) => formatHour(label as number)}
                        contentStyle={{
                            background: "rgba(0,0,0,0.8)",
                            border: "1px solid rgba(255,255,255,0.1)",
                            borderRadius: "8px",
                            color: "#f1f5f9",
                            fontSize: 12,
                        }}
                        cursor={{ stroke: "rgba(255,255,255,0.2)" }}
                    />

                    {/* monotone smooths the line between hours — looks much better than linear */}
                    <Area
                        type="monotone"
                        dataKey="temp"
                        stroke="#60a5fa"
                        strokeWidth={2}
                        fill="url(#tempGrad)"
                        dot={false}
                        activeDot={{ r: 4, fill: "#60a5fa", strokeWidth: 0 }}
                    />
                </AreaChart>
            </ResponsiveContainer>
        </div>
    );
}
