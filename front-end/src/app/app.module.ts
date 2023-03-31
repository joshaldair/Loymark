import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HistorialComponent } from './historial/historial.component';
import { HistorialModalComponent } from './historial/modal/historial-modal/historial-modal.component';
import { UserComponent } from './user/user.component';
import { UserModalComponent } from './user/modal/user-modal/user-modal.component';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    HistorialComponent,
    HistorialModalComponent,
    UserComponent,
    UserModalComponent
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserModule, 
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
