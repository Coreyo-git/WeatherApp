export interface WeatherCondition {
  code: number;
  text: string;
  iconUrl: string;
}

export interface WeatherSnapshot {
  time: string;
  isDay: boolean;
  condition: WeatherCondition;
  temperatureCelsius: number;
  feelsLikeCelsius: number;
  windSpeedKph: number;
  windDirection: string;
  humidity: number;
  uvIndex: number;
}

export interface DailyForecastSummary {
  maxTemperatureCelsius: number;
  minTemperatureCelsius: number;
  chanceOfRain: number;
  chanceOfSnow: number;
  condition: WeatherCondition;
}

export interface ForecastDay {
  date: string;
  summary: DailyForecastSummary;
  sunrise: string;
  sunset: string;
}

export interface Location {
  name: string;
  region: string;
  country: string;
}

export interface WeatherForecast {
  location: Location;
  current: WeatherSnapshot;
  today: ForecastDay;
}

export interface CitySearchResult {
    id: number;
    name: string;
    region: string;
    country: string;
    latitude: number;
    longitude: number;
} 
