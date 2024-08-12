import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountsService } from './_services/accounts.service';
import { HomeComponent } from "./home/home.component";
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, NavComponent, HomeComponent, NgxSpinnerComponent]
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountsService);

  ngOnInit(): void {
    this.setCurrentUSer();
  }

  setCurrentUSer(){
    const userString = localStorage.getItem('user');

    //is there a user? No
    if (!userString) {return};

    //yes
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }



}
