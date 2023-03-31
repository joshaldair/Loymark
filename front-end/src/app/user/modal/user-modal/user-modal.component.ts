import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../../service/user.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-user-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.css']
})
export class UserModalComponent {

  form: FormGroup = new FormGroup({});
  isSubmitted: boolean = false;
  listadoPaises:any;
  @Input() usuario: any;
  @Output() guardado: EventEmitter<boolean> = new EventEmitter();

  constructor(public modal: NgbActiveModal,
    private fb: FormBuilder,
    private service: UserService) { }

  ngOnInit(): void {
    this.getPaises();
    this.form = this.fb.group({
      id: [0],
      userName: [null, [Validators.required, Validators.maxLength(30)]],
      lastName: [null, [Validators.required, Validators.maxLength(30)]],
      email: [null, [Validators.required, Validators.email]],
      birthDay: [null, [Validators.required]],
      cellphone: [null, [Validators.pattern(/^-?(0|[1-9]\d*)?$/), Validators.max(999999999999)]],
      country: [null, [Validators.required, Validators.maxLength(3)]],
      contactInfo: [true],
    });

    if (!!this.usuario) {
      this.form.patchValue(this.usuario);
      this.form.controls['birthDay'].patchValue(this.formatDate(new Date(this.usuario.birthDay)));
    }
  }

  getPaises() {
    fetch('./assets/countries.json').then(res => res.json())
      .then(jsonData => {
        this.listadoPaises = jsonData;
      });
  }

  onSubmit = () => {
    this.isSubmitted = true;
    if (this.form.valid) {
      this.service.saveOrUpdateUsuario(this.form.value).subscribe(_ => {
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

  formatDate(date: any) {
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
  }

}
