import { Searchbar } from "./Searchbar";

interface Props {
    location: string;
    onSelectedSearchResult: (id: number) => void;
}

export function Header({ location, onSelectedSearchResult }: Props) {
  return (
      <div>
		  <h1>Weather Forecast</h1>
		  <p>{location}</p>
		  <Searchbar onSelectedSearchResult={onSelectedSearchResult} />
      </div>
  );
}