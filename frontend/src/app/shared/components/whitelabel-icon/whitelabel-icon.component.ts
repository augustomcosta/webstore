import { AfterViewInit, Component, Input } from '@angular/core';
import feather from 'feather-icons';
import { tv } from 'tailwind-variants';

@Component({
  selector: 'app-whitelabel-icon',
  standalone: true,
  template: `
    <i
      [class]="iconClass({ size: size, color: color, cursor: cursor })"
      [attr.data-feather]="icon"
    >
    </i>
  `,
})
export class WhitelabelIconComponent implements AfterViewInit {
  @Input() icon: string = '';
  @Input() size: 'sm' | 'md' | 'lg' = 'sm';
  @Input() color: 'white' | 'primary' | 'secondary' | 'active' | 'delete' =
    'primary';
  @Input() cursor: 'normal' | 'pointer' = 'normal';

  iconClass = tv({
    variants: {
      color: {
        white: 'text-white',
        primary: 'text-teal-800',
        secondary: 'text-amber-800',
        active: '',
        delete: '',
      },
      size: {
        sm: 'size-sm',
        md: 'size-md',
        lg: 'size-lg',
      },
      weight: {
        normal: 'font-normal',
        semibold: 'font-semibold',
      },
      cursor: {
        normal: 'cursor-normal',
        pointer: `cursor-pointer`,
      },
    },
    defaultVariants: {
      size: this.size,
      color: this.color,
    },
  });

  ngAfterViewInit() {
    console.log('ngAfterViewInit called - initializing feather icons');
    feather.replace();
  }
}
