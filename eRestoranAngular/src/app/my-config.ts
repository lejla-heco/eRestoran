import {HttpHeaders} from "@angular/common/http";

export class MyConfig{
  static adresaServera = "https://erestoran-api.p2102.app.fit.ba";
  static http_opcije= {
    headers: new HttpHeaders({"Content-Type":"multipart/form-data"})
  };
}
