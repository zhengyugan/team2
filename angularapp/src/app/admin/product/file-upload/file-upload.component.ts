import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Observable } from 'rxjs';
import { HttpEventType, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'pm-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit{
  
  selectedFiles?: FileList;
  currentFile?: File;
  message = '';
  preview = '';

  imageInfos?: Observable<any>;

  constructor(private uploadService: ProductService) {}

  ngOnInit(): void {
    this.imageInfos = this.uploadService.getFiles();
  }

  selectFile(event: any): void {
    this.message = '';
    this.preview = '';
    this.selectedFiles = event.target.files;
  
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
  
      if (file) {
        this.preview = '';
        this.currentFile = file;
  
        const reader = new FileReader();
  
        reader.onload = (e: any) => {
          console.log(e.target.result);
          this.preview = e.target.result;
        };
  
        reader.readAsDataURL(this.currentFile);
      }
    }
  }

  upload(): void {
  
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
  
      if (file) {
        this.currentFile = file;
  
        this.uploadService.upload(this.currentFile).subscribe({
          next: (event: any) => {
            if (event instanceof HttpResponse) {
              this.message = event.body.message;
              this.imageInfos = this.uploadService.getFiles();
            }
          },
          error: (err: any) => {
            console.log(err);
  
            if (err.error && err.error.message) {
              this.message = err.error.message;
            } else {
              this.message = 'Could not upload the image!';
            }
  
            this.currentFile = undefined;
          },
        });
      }
  
      this.selectedFiles = undefined;
    }
  }
}
