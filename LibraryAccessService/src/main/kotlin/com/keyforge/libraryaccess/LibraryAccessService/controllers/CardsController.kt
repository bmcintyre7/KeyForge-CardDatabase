package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.*
import com.keyforge.libraryaccess.LibraryAccessService.repositories.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardBody
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
    fun postCard(@RequestBody card : CardBody) : String {

        //val c: CardListBody = cards
        val responseData = mutableListOf<String>()
        var theType: Type?
        try {
            theType = typeRepository.findByName(card.type)
        } catch (e: Exception) {
            theType = Type(null, card.type)
            typeRepository.saveAndFlush(theType)
        }
        var theRarity: Rarity?
        try {
            theRarity = rarityRepository.findByName(card.rarity)
        } catch (e: Exception) {
            theRarity = Rarity(null, card.rarity)
            rarityRepository.saveAndFlush(theRarity)
        }
        //for (card in c.cards) {
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
            var theExpansion: Expansion?
            try {
                theExpansion = expansionRepository.findByName(setAndNumber[0])
            } catch (e: Exception) {
                theExpansion = Expansion(null, setAndNumber[0])
                expansionRepository.saveAndFlush(theExpansion)
            }
            val cardExpansions = CardExpansions(
                null,
                inserted,
                theExpansion!!,
                setAndNumber[1]
            )
            cardExpansionsRepository.saveAndFlush(cardExpansions)
        }

        for (house in card.houses) {
            var theHouse: House?
            try {
                theHouse = houseRepository.findByName(house)
            } catch (e: Exception) {
                theHouse = House(null, house)
                houseRepository.saveAndFlush(theHouse)
            }
            val cardHouses = CardHouses(
                null,
                inserted,
                theHouse!!
            )
            cardHousesRepository.saveAndFlush(cardHouses)
        }

        for (trait in card.traits) {
            var theTrait: Trait?
            try {
                theTrait = traitRepository.findByName(trait)
            } catch (e: Exception) {
                theTrait = Trait(null, trait)
                traitRepository.saveAndFlush(theTrait)
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
                theKeyword = Keyword(null, keyword)
                keywordRepository.saveAndFlush(theKeyword)
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

    @RequestMapping(value ="/cards/{expansion}/{id}", method = [RequestMethod.GET])
    fun getCardByNumber(@PathVariable("expansion") exp: String, @PathVariable("id") id: Int) : CardBody? {
        val expansions = cardExpansionsRepository.findByNumber(Integer.toString(id))
        for (cardExpansion in expansions) {
            if (cardExpansion.expansion.name.toLowerCase() == exp.toLowerCase())
                return cardToCardBody(cardRepository.findById(cardExpansion.card.id!!).get())
        }
        return null
    }

    @RequestMapping(value="/cards/house/{house}", method = [RequestMethod.GET])
    fun getCardsByHouse(@PathVariable("house") house: String): List<CardBody> {
        val theHouse = houseRepository.findByName(house)
        val theCardHouses = cardHousesRepository.findByHouseId(theHouse.id!!)
        val responseData = mutableListOf<CardBody>()
        for (cardHouse in theCardHouses) {
            responseData.add(cardToCardBody(cardHouse.card))
        }
        return responseData
    }

    fun cardToCardBody(card: Card): CardBody {
        val cardExpansions = cardExpansionsRepository.findByCardId(card.id!!)
        val cardHouses = cardHousesRepository.findByCardId(card.id!!)
        val cardKeywords = cardKeywordsRepository.findByCardId(card.id!!)
        val cardTraits = cardTraitsRepository.findByCardId(card.id!!)

        var expansions = mutableListOf<String>()
        var houses = mutableListOf<String>()
        var keywords = mutableListOf<String>()
        var traits = mutableListOf<String>()

        for (expansion in cardExpansions) {
            expansions.add(expansion.expansion.name)
        }

        for (house in cardHouses) {
            houses.add(house.house.name)
        }

        for (keyword in cardKeywords) {
            keywords.add(keyword.keyword.name)
        }

        for (trait in cardTraits) {
            traits.add(trait.trait.name)
        }

        return CardBody(
            card.name,
            card.type.name,
            card.text,
            card.aember,
            card.armor,
            card.power,
            card.rarity.name,
            card.artist,
            expansions,
            houses,
            keywords,
            traits
        )
    }
}