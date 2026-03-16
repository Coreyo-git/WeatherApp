import { useState } from "react";
import { getForecast } from "./api/forecast";
import { ForecastSummary } from "./components/ForecastSummary";
import type { WeatherForecast } from "./types/weather";

function App() {
  const [input, setInput] = useState("");
  const [forecast, setForecast] = useState<WeatherForecast | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function handleSearch(e: React.FormEvent) {
    e.preventDefault();
    if (!input.trim()) return;

    setLoading(true);
    setError(null);
    setForecast(null);

    try {
      setForecast(await getForecast(input.trim()));
    } catch (err) {
      setError(err instanceof Error ? err.message : "Something went wrong.");
    } finally {
      setLoading(false);
    }
  }

  return (
    <main>
      <h1>WeatherApp</h1>

      <form onSubmit={handleSearch}>
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Enter a city name..."
        />
        <button type="submit" disabled={loading}>
          {loading ? "Loading…" : "Search"}
        </button>
      </form>

      {error && <p>{error}</p>}

      {forecast && (
        <section>
          <h2>
            {forecast.location.name}, {forecast.location.region},{" "}
            {forecast.location.country}
          </h2>

          <div>
            <img
              src={`https:${forecast.current.condition.iconUrl}`}
              alt={forecast.current.condition.text}
            />
            <p>{forecast.current.condition.text}</p>
            <p>{forecast.current.temperatureCelsius}°C</p>
            <p>Feels like {forecast.current.feelsLikeCelsius}°C</p>
            <p>Wind {forecast.current.windSpeedKph} kph {forecast.current.windDirection}</p>
            <p>Humidity {forecast.current.humidity}%</p>
            <p>UV {forecast.current.uvIndex}</p>
          </div>

          <ForecastSummary today={forecast.today} />
        </section>
      )}
    </main>
  );
}

export default App;
