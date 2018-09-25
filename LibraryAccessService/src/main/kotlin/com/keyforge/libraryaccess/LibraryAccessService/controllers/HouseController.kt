package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import com.keyforge.libraryaccess.LibraryAccessService.repositories.HouseRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import org.springframework.web.bind.annotation.*

@RestController
class HouseController (
        private val houseRepository: HouseRepository
) {
    @RequestMapping(value ="/house/{id}", method = [RequestMethod.GET])
    fun getHouse(@PathVariable("id") id: Int) : String {
        return houseRepository.getOne(id).name
    }
}