import AsyncSelect from "react-select/async";
import type { SingleValue } from "react-select";
import { searchCities } from "../../api/cities";
import type { CitySearchResult } from "../../types/weather";

interface Props {
    // Callback invoked with the selected city's ID when the user picks a result.
    onSelectedSearchResult: (id: number) => void;
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
        if (option) onSelectedSearchResult(option.value);
    }

    return (
        // noOptionsMessage returns null so no "No options" text appears while the
        // user is still typing or when the input is below the minimum length.
        <AsyncSelect
            loadOptions={loadOptions}
			onChange={handleChange}
			isClearable={true}
            placeholder="Enter a city name..."
            noOptionsMessage={() => null}
        />
    );
}
