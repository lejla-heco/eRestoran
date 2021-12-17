import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Injectable({
  providedIn: 'root'
})
export class AutorizacijaDostavljacProvjera implements CanActivate{
  constructor(private router : Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    try {
      if(AutentifikacijaHelper.getLoginInfo().isPermisijaDostavljac)
        return true;
    }
    catch (e){
    }

    this.router.navigate(['home-page'], { queryParams: { returnUrl: state.url }});
    return false;
  }
}
