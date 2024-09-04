import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  getItemByKey<T = unknown>(key:string):T | undefined{
    const item = localStorage.getItem(key);
    
    if(item !== null){
      const value = JSON.parse(item) as T;

      return value;
    }

    return undefined;
  }

  removeItemByKey(key:string){
    localStorage.removeItem(key);
  }

  setItemByKey(key:string,item:unknown){
    const value = JSON.stringify(item);

    localStorage.setItem(key,value);
  }

  RemoveAll(){
    localStorage.clear();
  }
}
