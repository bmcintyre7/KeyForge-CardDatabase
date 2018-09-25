package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import com.keyforge.libraryaccess.LibraryAccessService.repositories.RarityRepository
import com.keyforge.libraryaccess.LibraryAccessService.repositories.TypeRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import org.springframework.web.bind.annotation.*

@RestController
class CardsController (
        private val cardRepository: CardRepository,
        private val typeRepository: TypeRepository,
        private val rarityRepository: RarityRepository
) {
    @RequestMapping(value ="/cards", method = [RequestMethod.POST])
    fun postCards(@RequestBody cards : CardListBody) : String {

        val c: CardListBody = cards
        for (card in c.cards) {
            var toAdd = Card(
                null,
                    card.name,
                    typeRepository.findByName(card.type),
                    card.text,
                    card.aember,
                    card.power,
                    card.armor,
                    rarityRepository.findByName(card.rarity),
                    card.artist
            )

            cardRepository.saveAndFlush(toAdd)
        }
        return "We doing it"
    }
}