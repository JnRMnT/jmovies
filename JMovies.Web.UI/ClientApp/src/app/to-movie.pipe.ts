import { Pipe, PipeTransform } from '@angular/core';
import { Movie } from './models/general-models/movie';

@Pipe({
  name: 'toMovie'
})
export class ToMoviePipe implements PipeTransform {

  transform(value: any, ...args: any[]): Movie {
    return <Movie>value;
  }

}
