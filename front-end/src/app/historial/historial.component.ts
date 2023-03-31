import { Component } from '@angular/core';
import { HistorialService } from './service/historial.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HistorialModalComponent } from './modal/historial-modal/historial-modal.component';

@Component({
  selector: 'app-historial',
  templateUrl: './historial.component.html',
  styleUrls: ['./historial.component.css']
})
export class HistorialComponent {

  activityList: any;

  constructor(private modalService: NgbModal,
    private service: HistorialService) { }

  ngOnInit(): void {
    this.getActivity();
  }

  getActivity = () =>
    this.service.getActivities().subscribe(res => this.activityList = res);


  deleteActivity = (id: number) => {
    if (confirm("Estas seguro de eliminar el registro ")) {
      this.service.deleteActividad(id).then(() => {
        alert("Registro borrado")
        this.getActivity();
      });

    }
  }

  AddOrEdit = (item: any) => {
    const modalRef = this.modalService.open(HistorialModalComponent);
    modalRef.componentInstance.actividad = item;
    modalRef.componentInstance.guardado.subscribe((receivedEntry: any) => {
      if (receivedEntry)
        this.getActivity();
    })
  }

}
