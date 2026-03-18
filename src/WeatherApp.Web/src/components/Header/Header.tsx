import { Searchbar } from "./Searchbar";
import type { ForecastQuery } from "../../types/weather";
import { Loader2, MapPin } from "lucide-react";

interface Props {
    location: string;
    isLoading: boolean;
    onSelectedSearchResult: (query: ForecastQuery) => void;
}
// TODO: Celsius / Fahrenheit toggle, double for both available from API
export function Header({ location, isLoading, onSelectedSearchResult }: Props) {
    return (
        // Layout: flex row, h1 left, location+searchbar right
        <header className="flex items-center justify-between gap-4 py-4">
            <h1 className="text-3xl font-bold">Weather Forecast</h1>
            <div className="flex items-end gap-4">
                {isLoading ? (
                    <Loader2 size={32} className="animate-spin" />
                ) : location && (
                    <p className="flex items-center gap-1 text-sm">
                        <MapPin size={20} />
                        {location}
                    </p>
                )}
                <div className="w-72">
                    <Searchbar
                        onSelectedSearchResult={onSelectedSearchResult}
                    />
                </div>
            </div>
        </header>
    );
}
