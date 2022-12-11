import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product!: IProduct;
  productRouteId: number;

  constructor(private shopService: ShopService, private activateRoute:ActivatedRoute, private bcService: BreadcrumbService) { 
    this.productRouteId = Number(activateRoute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    this.loadProduct()
  }

  loadProduct(){
    this.shopService.getProduct(this.productRouteId).subscribe( product => {
      this.product = product;
      this.bcService.set('@productDetails',product.name);
    }, error => {
      console.error(error)
    })
  }

}
