import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Actividad } from '../../service/actividad.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HistorialService } from '../../service/historial.service';

@Component({
  selector: 'app-historial-modal',
  templateUrl: './historial-modal.component.html',
  styleUrls: ['./historial-modal.component.css']
})
export class HistorialModalComponent {

  @Input() actividad: any;
  @Output() guardado: EventEmitter<boolean> = new EventEmitter();
  form: FormGroup = new FormGroup({
    id: new FormControl(),
    activityName: new FormControl(),
    userId: new FormControl()
  });
  isSubmitted: boolean = false;
  listaUsuarios: any;

  constructor(public modal: NgbActiveModal,
    private fb: FormBuilder,
    private service: HistorialService) { }

  ngOnInit(): void {
    this.getUsuarios();
    this.form = this.fb.group({
      id: [0],
      activityName: [null, [Validators.required]],
      userId: [null, [Validators.required]],
    });

    if (!!this.actividad) {
      this.form.patchValue(this.actividad);
    }

  }

  getUsuarios = () => this.service.getUsuarios().subscribe(res => this.listaUsuarios = res);

  onSubmit = () => {
    this.isSubmitted = true;
    if (this.form.valid) {
      this.service.saveOrUpdateActividad(this.form.value).subscribe(_ => {
        alert("Datos guardado con exito");
        this.form.reset();
        this.modal.close();
        this.guardado.emit(true)
      }, (err: any) => {
        this.guardado.emit(false)
        alert(err.error.Details)
        //alert("Error al guardar los datos");
      })
    }
  }

}
