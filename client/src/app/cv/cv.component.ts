import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cv',
  templateUrl: './cv.component.html',
  styleUrls: ['./cv.component.css']
})
export class CvComponent{
  Name = 'Angular';

  ngOnInit(): void {
  }

  openFile() {
    console.log('hello')
    document.querySelector('input')?.click();
  }
  fileSelected(event: any) {
    const selectedFile = event.target.files[0]; // Get the first selected file
    if (selectedFile) {
      // You can now work with the selected file, e.g., upload it to a server or process it.
      console.log('Selected File:', selectedFile);
    }
  }
}
