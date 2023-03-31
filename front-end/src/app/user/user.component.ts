import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from './service/user.service';
import { UserModalComponent } from './modal/user-modal/user-modal.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {

  listadoUsuarios: any

  constructor(private modalService: NgbModal,
    private service: UserService) { }

  ngOnInit(): void {
    this.getUsuarios();
  }

  getUsuarios = () => {
    this.service.getUsuarios().subscribe(res => this.listadoUsuarios = res);
  }

  AddOrEdit = (item: any) => {
    const modalRef = this.modalService.open(UserModalComponent);
    modalRef.componentInstance.usuario = item;
    modalRef.componentInstance.guardado.subscribe((receivedEntry: any) => {
      if (receivedEntry)
        this.getUsuarios();
    })
  }

  deleteUser = (id: number) => {
    if (confirm("Estas seguro de eliminar el registro ")) {
      this.service.deleteUsuario(id).then(() => {
        alert("Registro borrado")
        this.getUsuarios();
      });

    }
  }

}
