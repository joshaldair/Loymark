import { UserComponent } from './user/user.component';
import { HistorialComponent } from './historial/historial.component';
import { RouterModule, Routes } from "@angular/router";
import { NgModule } from '@angular/core';

const routes: Routes = [
    { path: '', redirectTo: '/historial', pathMatch: 'full' },
    { path: 'historial', component:  HistorialComponent },
    {
        path: 'usuario', component: UserComponent
   
    },

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }