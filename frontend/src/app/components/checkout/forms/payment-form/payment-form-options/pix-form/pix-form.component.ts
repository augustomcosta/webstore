import { Component } from '@angular/core';
import { QRCodeModule } from 'angularx-qrcode';

@Component({
  selector: 'app-pix-form',
  standalone: true,
  imports: [QRCodeModule],
  templateUrl: './pix-form.component.html',
  styleUrl: './pix-form.component.css',
})
export class PixFormComponent {
  pixQrCode =
    'https://audaces.com/pt-br/solucoes/audaces-360?gad_source=1&gclid=EAIaIQobChMI-oqy5fyKhgMV0jjUAR18bgN5EAAYASAAEgJnn_D_BwE';
}
