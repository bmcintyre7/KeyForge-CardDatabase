package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.*
import com.keyforge.libraryaccess.LibraryAccessService.repositories.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import org.springframework.web.bind.annotation.*

@RestController
class CardsController (
        private val cardRepository: CardRepository,
        private val typeRepository: TypeRepository,
        private val rarityRepository: RarityRepository,
        private val cardExpansionsRepository: CardExpansionsRepository,
        private val cardHousesRepository: CardHousesRepository,
        private val cardKeywordsRepository: CardKeywordsRepository,
        private val cardTraitsRepository: CardTraitsRepository,
        private val expansionRepository: ExpansionRepository,
        private val keywordRepository: KeywordRepository,
        private val houseRepository: HouseRepository,
        private val traitRepository: TraitRepository

) {
    @RequestMapping(value ="/cards", method = [RequestMethod.POST])
    fun postCards(@RequestBody cards : CardListBody) : String {

        val c: CardListBody = cards
        val responseData = mutableListOf<String>()
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

            responseData.add(card.name)

            val inserted = cardRepository.saveAndFlush(toAdd)
            for (expansion in card.expansions) {
                val setAndNumber = expansion.split(" #")
                val cardExpansions = CardExpansions(
                    null,
                    inserted,
                    expansionRepository.findByName(setAndNumber[0]),
                    setAndNumber[1]
                )
                cardExpansionsRepository.saveAndFlush(cardExpansions)
            }

            for (house in card.houses) {
                val cardHouses = CardHouses(
                    null,
                    inserted,
                    houseRepository.findByName(house)
                )
                cardHousesRepository.saveAndFlush(cardHouses)
            }

            for (trait in card.traits) {
                val cardTraits = CardTraits(
                        null,
                        inserted,
                        traitRepository.findByName(trait)
                )
                cardTraitsRepository.saveAndFlush(cardTraits)
            }

            for (keyword in card.keywords) {
                val cardKeywords = CardKeywords(
                        null,
                        inserted,
                        keywordRepository.findByName(keyword)
                )
                cardKeywordsRepository.saveAndFlush(cardKeywords)
            }
        }
        return "Added:\n-------\n" + responseData.joinToString(",\n")
    }
}