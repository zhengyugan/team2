import { Component, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({ 
  selector: 'pm-root', 
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
})
export class AppComponent {
  constructor(private modalService: NgbModal) {}
  open()
  {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.customMessage = 'Test Message';

  }
}


@Component({
  selector: 'ngbd-modal-content',
  templateUrl: './modal/success-modal/modal.component.html',
})
export class NgbdModalContent {
  @Input() customMessage: any;
  constructor(public activeModal: NgbActiveModal){}
}

export class NgbdModalComponent {
	constructor(private modalService: NgbModal) {}

	open() {
		const modalRef = this.modalService.open(NgbdModalContent);
	}
}
