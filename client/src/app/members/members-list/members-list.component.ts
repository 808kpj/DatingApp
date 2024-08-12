import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/members';
import { MembersCardComponent } from '../members-card/members-card.component';
import { PageChangedEvent, PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_models/UserParams';
import { AccountsService } from '../../_services/accounts.service';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [MembersCardComponent, PaginationModule,FormsModule, ButtonsModule],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})

export class MembersListComponent implements OnInit {
  private accountService = inject(AccountsService);
  memberService = inject(MembersService);
  userParams = new UserParams(this.accountService.currentUser());
  genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'Females'}];

  ngOnInit(): void {
    if(!this.memberService.paginatedResult()) this.loadMembers();
  }

  resetFilters() {
    this.userParams = new UserParams(this.accountService.currentUser());
    this.loadMembers();
  }

pageChanged(event: any) {
  if (this.userParams.pageNumber !== event.page) {
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }
 }

  loadMembers() {
    this.memberService.getMembers(this.userParams)
  }

}
