package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.*
import com.keyforge.libraryaccess.LibraryAccessService.repositories.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardBody
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import org.springframework.web.bind.annotation.*
import java.lang.Exception

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
    fun postCards(@RequestBody card : CardBody) : String {
        //val c: CardListBody = cards
        val responseData = mutableListOf<String>()
        //for (card in c.cards) {
            var theType: Type?
            try {
                theType = typeRepository.findByName(card.type)
            } catch (e: Exception) {
                theType = typeRepository.saveAndFlush(Type(null, card.type))
            }

            var theRarity: Rarity?
            try {
                theRarity = rarityRepository.findByName(card.rarity)
            } catch (e:Exception) {
                theRarity = rarityRepository.saveAndFlush(Rarity(null, card.rarity))
            }

            var toAdd = Card(
                    null,
                    card.name,
                    theType!!,
                    card.text,
                    card.aember,
                    card.power,
                    card.armor,
                    theRarity!!,
                    card.artist
            )

            responseData.add(card.name)

            val inserted = cardRepository.saveAndFlush(toAdd)
            for (expansion in card.expansions) {
                val setAndNumber = expansion.split(" #")
                val ex = expansionRepository.findByName(setAndNumber[0])
                val cardExpansions = CardExpansions(
                        null,
                        inserted,
                        ex,
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
                var theTrait: Trait?
                try {
                    theTrait = traitRepository.findByName(trait)
                } catch (e: Exception) {
                    theTrait = traitRepository.saveAndFlush(Trait(null, trait))
                }

                val cardTraits = CardTraits(
                        null,
                        inserted,
                        theTrait!!
                )
                cardTraitsRepository.saveAndFlush(cardTraits)
            }

            for (keyword in card.keywords) {
                var theKeyword: Keyword?
                try {
                    theKeyword = keywordRepository.findByName(keyword)
                } catch (e: Exception) {
                    theKeyword = keywordRepository.saveAndFlush(Keyword(null, keyword))
                }
                val cardKeywords = CardKeywords(
                        null,
                        inserted,
                        theKeyword!!
                )
                cardKeywordsRepository.saveAndFlush(cardKeywords)
            }
        //}
        return "Added:\n-------\n" + responseData.joinToString(",\n")
    }
}
