package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import com.keyforge.libraryaccess.LibraryAccessService.repositories.HouseRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import jdk.nashorn.internal.objects.NativeJSON.stringify
import com.google.gson.Gson
import org.springframework.web.bind.annotation.*

@RestController
class HouseController (
        private val houseRepository: HouseRepository
) {
    @RequestMapping(value ="/houses/{id}", method = [RequestMethod.GET])
    fun getHouse(@PathVariable("id") id: Int) : String {
        return houseRepository.getOne(id).name
    }

    @RequestMapping(value ="/houses", method = [RequestMethod.GET])
    fun getHouses() : String {
        val gson = Gson()
        var houses = houseRepository.findAll()
        var responseData = mutableListOf<String>()
        for (house in houses) {
            responseData.add(house.name)
        }
        return responseData.joinToString(", ")
    }
}