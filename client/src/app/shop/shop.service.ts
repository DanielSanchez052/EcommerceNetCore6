import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBrand } from '../shared/models/brands';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { delay, map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService
{
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams): Observable<IPagination<IProduct>>
  {
    let params = new HttpParams();

    if (shopParams.brandId !== 0)
    {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0)
    {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if(shopParams.search){
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort)

    //pagination
    params = params.append('PageIndex', shopParams.pageNumber.toString());
    params = params.append('PageSize', shopParams.pageSize.toString());


    return this.http.get<IPagination<IProduct>>(this.baseUrl + 'products', { observe: 'body', params })
    // .pipe(
    //   delay(1000),
    //   map(response =>
    //   {
    //     return response.body;
    //   })
    // )
  }

  getBrands(): Observable<IBrand[]>
  {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }


  getProductTypes(): Observable<IType[]>
  {
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
  }
}
