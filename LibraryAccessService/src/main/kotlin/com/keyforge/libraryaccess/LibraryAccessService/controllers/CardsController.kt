package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import org.springframework.web.bind.annotation.*

@RestController
class CardsController (
        private val cardRepository: CardRepository
) {
    @RequestMapping(value ="/cards", method = [RequestMethod.POST])
    fun postCards(@RequestBody cards : CardListBody) : String {

        val c: CardListBody = cards
        //for (card in c.cards) {
            
        //}
        return cardRepository.toString()
    }
}