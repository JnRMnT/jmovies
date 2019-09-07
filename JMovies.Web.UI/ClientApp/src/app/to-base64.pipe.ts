import { Pipe, PipeTransform } from '@angular/core';
import { Image } from './models/general-models/image';
import { JM } from 'jm-utilities';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Pipe({
  name: 'toBase64'
})
export class ToBase64Pipe implements PipeTransform {
  constructor(public sanitizer: DomSanitizer) {
  }

  transform(value: Image, ...args: any[]): SafeResourceUrl {
    if (JM.isDefined(value) && JM.isDefined(value.content)) {
      return this.sanitizer.bypassSecurityTrustResourceUrl("data:" + this.getMimeType(value) + ";base64, " + value.content.$value);
    } else {
      return "";
    }
  }

  private getMimeType(image: Image): string {
    let mimeType: string;
    var extension = image.url.split('.')[image.url.split('.').length - 1];
    if (extension == 'jpg') {
      mimeType = "image/jpeg";
    } else {
      mimeType = "image/" + extension;
    }

    return mimeType;
  }
}
