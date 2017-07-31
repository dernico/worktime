import { Component, OnInit } from '@angular/core';
import { LoginService } from "../../services/login.service";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css'],
  providers: [LoginService]
})
export class NavigationComponent implements OnInit {

  ngOnInit() {
  }


  private _loginService: LoginService;

  constructor(loginService: LoginService) {
    this._loginService = loginService;
  }

  logout() {
    this._loginService.logout();
  }

}
