import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  getItemByKey<T = unknown>(key:string):T | null{
    try{
      const item = localStorage.getItem(key);
    
      if(!item) return null;

      const value = JSON.parse(item) as T;

      return value;
    }catch(err:unknown){
      //TODO??: SEND VIA A MESSAGE BROKER THE ERROR (RabbitMQ?)

      return null;
    }
  }

  removeItemByKey(key:string):void{
    localStorage.removeItem(key);
  }

  setItemByKey(key:string,item:unknown):boolean{
    try{
      const value = JSON.stringify(item);

      localStorage.setItem(key,value);

      return true;
    }catch(err:unknown){
      //TODO??: SEND VIA A MESSAGE BROKER THE ERROR (RabbitMQ?)

      return false;
    }
  }

  purge():void{
    localStorage.clear();
  }
}