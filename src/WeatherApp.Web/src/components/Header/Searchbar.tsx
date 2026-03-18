import AsyncSelect from "react-select/async";
import { Search } from "lucide-react";
import type { SingleValue } from "react-select";
import { searchCities } from "../../api/cities";
import type { CitySearchResult, ForecastQuery } from "../../types/weather";
import { createForecastQuery } from "../../types/weather";

interface Props {
    // Callback invoked with the selected city's ID when the user picks a result.
    onSelectedSearchResult: (id: ForecastQuery) => void;
}

// Shape expected by react-select: value is the city ID, label is the display string.
type Option = { value: number; label: string };

// Maps a CitySearchResult to a react-select Option.
function toOption(r: CitySearchResult): Option {
    return { value: r.id, label: `${r.name}, ${r.region}, ${r.country}` };
}

export function Searchbar({ onSelectedSearchResult }: Props) {
    // Called by AsyncSelect on each keystroke. Returns an empty array until the
    // input is at least 3 characters to avoid unnecessary API calls.
    async function loadOptions(inputValue: string): Promise<Option[]> {
        if (!inputValue || inputValue.length < 3) return [];

        const results = await searchCities(inputValue);
        return results.map(toOption);
    }

    // Fires when the user selects a result from the dropdown.
    function handleChange(option: SingleValue<Option>) {
        if (option) {
            onSelectedSearchResult(createForecastQuery(option.value));
        }
    }

    return (
        // noOptionsMessage returns null so no "No options" text appears while the
        // user is still typing or when the input is below the minimum length.
        <AsyncSelect
            loadOptions={loadOptions}
            onChange={handleChange}
            isClearable={true}
            placeholder={<Search size={25} />}
            noOptionsMessage={() => null}
            styles={{
                control: (base) => ({
                    ...base,
                    backgroundColor: "rgba(167, 166, 166, 0.79)",
                    borderColor: "rgba(255,255,255,0.3)",
                    color: "#f1f5f9",
                    boxShadow: "none",
                    "&:hover": { borderColor: "rgba(255,255,255,0.6)" },
                }),
                input: (base) => ({ ...base, color: "#f1f5f9" }),
                placeholder: (base) => ({
                    ...base,
                    color: "rgba(241,245,249,0.6)",
                }),
                singleValue: (base) => ({ ...base, color: "#f1f5f9" }),
                menu: (base) => ({
                    ...base,
                    backgroundColor: "rgba(15,23,42,0.95)",
                    border: "1px solid rgba(255,255,255,0.15)",
                }),
                option: (base, state) => ({
                    ...base,
                    backgroundColor: state.isFocused
                        ? "rgba(255,255,255,0.1)"
                        : "transparent",
                    color: "#f1f5f9",
                    "&:active": { backgroundColor: "rgba(255,255,255,0.2)" },
                }),
            }}
        />
    );
}
