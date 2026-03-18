/** Formats a 24-hour integer (0–23) as "12am", "2pm", etc. */
export function formatHour(hour: number): string {
    if (hour === 0) return "12am";
    if (hour === 12) return "12pm";
    return hour < 12 ? `${hour}am` : `${hour - 12}pm`;
}

/** Formats a TimeOnly string ("HH:mm:ss") as "5:49am", "6:04pm", etc. */
export function formatTimeOnly(timeStr: string): string {
    const [hourStr, minuteStr] = timeStr.split(":");
    const hour = parseInt(hourStr, 10);
    if (hour === 0) return `12:${minuteStr}am`;
    if (hour === 12) return `12:${minuteStr}pm`;
    return hour < 12 ? `${hour}:${minuteStr}am` : `${hour - 12}:${minuteStr}pm`;
}
