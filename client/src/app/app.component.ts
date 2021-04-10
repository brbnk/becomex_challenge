import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

interface SearchOptions {
  value: string;
  viewValue: string;
}

interface Currency {
  code: string,
  name: string,
  symbol: string
}

interface Languages {
  isO639_1: string,
  isO639_2: string,
  name: string,
  nativeName: string
}

interface RegionalBloc {
  acronym: string,
  name: string,
  otherAcronym: string[],
  otherNames: string[]
}

interface Country {
  name: string,
  cioc: string,
  alpha3Code: string,
  flag: string,
  capital: string,
  population: number,
  borders: string[],
  timezones: string[],
  currencies: Currency[],
  languages: Languages[],
  regionalBlocs: RegionalBloc[]
}

interface Route {
  from: Country,
  to: Country
}

interface RouteOrder {
  order: number,
  country: Country
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private readonly apiUrl : string;
  public route: Route = {
    to: null,
    from: null
  };

  constructor(private http : HttpClient) {
    this.apiUrl = 'http://localhost:5000/api/countries'
  }

  public title: string = 'client'
  public selectedOption: string
  public search: string = null
  public panelOpenState = false
  public countries: Country[]
  public routeOrder: RouteOrder[] = []

  public options: SearchOptions[] = [
    { value: 'name', viewValue: 'Nome' },
    { value: 'code', viewValue: 'Sigla' },
    { value: 'currency', viewValue: 'Moeda' }
  ]

  public convertRegionalBlocsToString(regionalBlocs: RegionalBloc[]): string {
    const list = regionalBlocs.map(r =>`${r.name} - ${r.acronym}`)
    return `[ ${list.join(', ')} ]`
  }

  public convertCurrencyToString(currencies: Currency[]): string {
    const list = currencies.map(c => `${c.name} - ${c.symbol}`)
    return `[ ${list.join(', ')} ]`
  }

  public convertLanguagesToString(languages: Languages[]): string {
    const list = languages.map(l => `${l.nativeName}`)
    return `[ ${list.join(', ')} ]`
  }

  public select(country: Country): void {
    if (this.route && this.route.from) {
      this.route.to = country
      return
    }

    this.route.from = country
  }

  public clearCoutries(): void {
    this.route = {
      to: null,
      from: null
    }

    this.routeOrder = []
  }

  public getRoute(): void {
    const endpoint = `${this.apiUrl}/route`;

    this.http.post<RouteOrder[]>(endpoint, this.route)
      .subscribe(res => {
          this.routeOrder = res
        },
        err => {
          if (err.status == 500) {
            alert('Invalid Request')
          }
        }
      )
  }

  public getCountries() {
    if (!this.selectedOption || !this.search) {
      alert('Invalid input')
      return
    }

    this.countries = []
    const endpoint = `${this.apiUrl}/${this.selectedOption}/${this.search}`;

    if (this.selectedOption == 'code') {
      this.http.get<Country>(endpoint)
        .subscribe(res => {
            this.countries = [res]
          },
          err => {
            if (err.status == 500) {
              alert('Invalid Request')
            }
          }
        )
        return
    }

    this.http.get<Country[]>(endpoint)
      .subscribe(res => {
          this.countries = res
        },
        err => {
          if (err.status == 500) {
            alert('Invalid Request')
          }
        }
      )
  }
}
