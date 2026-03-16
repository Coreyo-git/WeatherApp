import type { ForecastDay } from "../types/weather";

interface Props {
  today: ForecastDay;
}

export function ForecastSummary({ today }: Props) {
  return (
    <div>
      <p>
        High {today.summary.maxTemperatureCelsius}°C · Low{" "}
        {today.summary.minTemperatureCelsius}°C
      </p>
      <p>Chance of rain {today.summary.chanceOfRain}%</p>
      <p>
        Sunrise {today.sunrise} · Sunset {today.sunset}
      </p>
    </div>
  );
}
