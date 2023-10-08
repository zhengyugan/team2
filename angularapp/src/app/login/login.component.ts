import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../helpers/validateform';

@Component({
  selector: 'pm-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  type: string ="password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup;
  constructor(private fb: FormBuilder){}

  ngOnInit(): void{
    this.loginForm = this.fb.group({
      email:['',Validators.required],
      password:['',Validators.required]
    })
  }

  hideShow(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-solid fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password"
  }

  onSubmit(){
    if(this.loginForm.valid){
      console.log(this.loginForm.value)
    }else{
      ValidateForm.validateInput(this.loginForm);
      alert("Your form is invalid")
    }
  }

  
}
