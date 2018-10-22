package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.fasterxml.jackson.annotation.JsonInclude
import com.keyforge.libraryaccess.LibraryAccessService.data.*
import com.keyforge.libraryaccess.LibraryAccessService.repositories.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardBody
import com.keyforge.libraryaccess.LibraryAccessService.responses.RarityBody
import com.keyforge.libraryaccess.LibraryAccessService.specifications.CardQuery
import org.springframework.web.bind.annotation.*
import com.fasterxml.jackson.module.kotlin.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.DetailedCardBody
import java.text.Normalizer
import org.modelmapper.ModelMapper
import org.springframework.beans.factory.annotation.Autowired
import java.util.stream.Collectors


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
        //private val modelMapper: ModelMapper
) {
    //@RequestMapping(value = "/cards", method = [RequestMethod.POST])
    //fun postCard(@RequestBody card : CardBody) : String {
//
    //    //val c: CardListBody = cards
    //    val responseData = mutableListOf<String>()
    //    var theType: Type?
    //    try {
    //        theType = typeRepository.findByName(card.type)
    //    } catch (e: Exception) {
    //        theType = Type(null, card.type)
    //        typeRepository.saveAndFlush(theType)
    //    }
    //    var theRarity: Rarity?
    //    try {
    //        theRarity = rarityRepository.findByName(card.rarity)
    //    } catch (e: Exception) {
    //        theRarity = Rarity(null, card.rarity)
    //        rarityRepository.saveAndFlush(theRarity)
    //    }
    //    //for (card in c.cards) {
    //    var toAdd = Card(
    //        null,
    //        card.name,
    //        theType!!,
    //        card.text,
    //        card.aember,
    //        card.power,
    //        card.armor,
    //        theRarity!!,
    //        card.artist
    //    )
//
    //    responseData.add(card.name)
//
    //    val inserted = cardRepository.saveAndFlush(toAdd)
    //    for (expansion in card.expansions) {
    //        val setAndNumber = expansion.split(" #")
    //        var theExpansion: Expansion?
    //        try {
    //            theExpansion = expansionRepository.findByName(setAndNumber[0])
    //        } catch (e: Exception) {
    //            theExpansion = Expansion(null, setAndNumber[0])
    //            expansionRepository.saveAndFlush(theExpansion)
    //        }
    //        val cardExpansions = CardExpansions(
    //            null,
    //            inserted,
    //            theExpansion!!,
    //            setAndNumber[1]
    //        )
    //        cardExpansionsRepository.saveAndFlush(cardExpansions)
    //    }
//
    //    for (house in card.houses) {
    //        var theHouse: House?
    //        try {
    //            theHouse = houseRepository.findByName(house)
    //        } catch (e: Exception) {
    //            theHouse = House(null, house)
    //            houseRepository.saveAndFlush(theHouse)
    //        }
    //        val cardHouses = CardHouses(
    //            null,
    //            inserted,
    //            theHouse!!
    //        )
    //        cardHousesRepository.saveAndFlush(cardHouses)
    //    }
//
    //    for (trait in card.traits) {
    //        var theTrait: Trait?
    //        try {
    //            theTrait = traitRepository.findByName(trait)
    //        } catch (e: Exception) {
    //            theTrait = Trait(null, trait)
    //            traitRepository.saveAndFlush(theTrait)
    //        }
    //        val cardTraits = CardTraits(
    //                null,
    //                inserted,
    //                theTrait!!
    //        )
    //        cardTraitsRepository.saveAndFlush(cardTraits)
    //    }
//
    //    for (keyword in card.keywords) {
    //        var theKeyword: Keyword?
    //        try {
    //            theKeyword = keywordRepository.findByName(keyword)
    //        } catch (e: Exception) {
    //            theKeyword = Keyword(null, keyword)
    //            keywordRepository.saveAndFlush(theKeyword)
    //        }
    //        val cardKeywords = CardKeywords(
    //            null,
    //            inserted,
    //            theKeyword!!
    //        )
    //        cardKeywordsRepository.saveAndFlush(cardKeywords)
    //    }
    //    //}
    //    return "Added:\n-------\n" + responseData.joinToString(",\n")
    //}

    @RequestMapping(value = "/cards/{expansion}/{id}", method = [RequestMethod.GET])
    fun getCardByNumber(@PathVariable("expansion") exp: String, @PathVariable("id") id: Int) : DetailedCardBody? {
        val expansions = cardExpansionsRepository.findByNumber(Integer.toString(id))
        for (cardExpansion in expansions) {
            if (cardExpansion.expansion.name.toLowerCase() == exp.toLowerCase())
                return cardRepository.findById(cardExpansion.card.id!!).get().toDetailedCardBody()
        }
        return null
    }

    @RequestMapping(value = "/cards/house/{house}", method = [RequestMethod.GET])
    fun getCardsByHouse(@PathVariable("house") house: String): List<DetailedCardBody> {
        val theHouse = houseRepository.findByName(house)
        val theCardHouses = cardHousesRepository.findByHouseId(theHouse.id!!)
        val responseData = mutableListOf<DetailedCardBody>()
        for (cardHouse in theCardHouses) {
            responseData.add(cardHouse.card.toDetailedCardBody())
        }
        return responseData
    }

    @RequestMapping(value = "/cards", method = [RequestMethod.GET])
    @ResponseBody
    fun getCards(@RequestParam(value = "name", required = false) name: String?,
                 @RequestParam(value = "text", required = false) text: String?,
                 @RequestParam(value = "aember", required = false) aember: String?,
                 @RequestParam(value = "power", required = false) power: String?,
                 @RequestParam(value = "armor", required = false) armor: String?,
                 @RequestParam(value = "artist", required = false) artist: String?,
                 @RequestParam(value = "types", required = false) types: MutableList<String>?,
                 @RequestParam(value = "keywords", required = false) keywords: MutableList<String>?,
                 @RequestParam(value = "traits", required = false) traits: MutableList<String>?,
                 @RequestParam(value = "houses", required = false) houses: MutableList<String>?,
                 @RequestParam(value = "rarities", required = false) rarities: MutableList<String>?) : ByteArray {
        // TODO: This needs optimized, a lot.
        var queryRarities = mutableListOf<Rarity>()
        var queryTypes = mutableListOf<Type>()
        var queryHouses = mutableListOf<House>()
        var queryKeywords = mutableListOf<Keyword>()
        if (null != rarities)
            for (rarity in rarities) {
                queryRarities.add(rarityRepository.findByName(rarity))
            }
        if (null != types)
            for (type in types) {
                queryTypes.add(typeRepository.findByName(type))
            }
        if (null != houses)
            for (house in houses) {
                queryHouses.add(houseRepository.findByName(house))
            }
        if (null != keywords)
            for (keyword in keywords) {
                val kw = keywordRepository.findByName(keyword)
                //val matches = cardKeywordsRepository.findByKeywordId(kw.id!!)
                //for (match in matches)
                    queryKeywords.add(kw)
            }

        var aemberValue = mutableMapOf<String, String>()
        if (null != aember) {
            if (aember.contains(">="))
                aemberValue.put("GTE", aember.substring(2))
            else if (aember.contains(">"))
                aemberValue.put("GT", aember.substring(1))
            else if (aember.contains("<="))
                aemberValue.put("LTE", aember.substring(2))
            else if (aember.contains("<"))
                aemberValue.put("LT", aember.substring(1))
            else if (aember.contains("="))
                aemberValue.put("E", aember.substring(1))
        }
        
        var powerValue = mutableMapOf<String, String>()
        if (null != power) {
            if (power.contains(">="))
                powerValue.put("GTE", power.substring(2))
            else if (power.contains(">"))
                powerValue.put("GT", power.substring(1))
            else if (power.contains("<="))
                powerValue.put("LTE", power.substring(2))
            else if (power.contains("<"))
                powerValue.put("LT", power.substring(1))
            else if (power.contains("="))
                powerValue.put("E", power.substring(1))
        }
        
        var armorValue = mutableMapOf<String, String>()
        if (null != armor) {
            if (armor.contains(">="))
                armorValue.put("GTE", armor.substring(2))
            else if (armor.contains(">"))
                armorValue.put("GT", armor.substring(1))
            else if (armor.contains("<="))
                armorValue.put("LTE", armor.substring(2))
            else if (armor.contains("<"))
                armorValue.put("LT", armor.substring(1))
            else if (armor.contains("="))
                armorValue.put("E", armor.substring(1))
        }

        val query = CardQuery(name = name, types = queryTypes, rarities = queryRarities, text = text, houses = queryHouses, keywords = queryKeywords, aember = aemberValue, power = powerValue, armor = armorValue)
        val results = cardRepository.findAll(query.toSpecification())

        val mapper = jacksonObjectMapper()
                .registerKotlinModule()
                .setSerializationInclusion(JsonInclude.Include.NON_NULL)
        val l = results.stream().map { card -> card.toCardBody() }.collect(Collectors.toList())
        return mapper.writeValueAsBytes(l)
       //var filteredCards = CardListBody(mutableListOf())

       //for (card in results) {
       //    filteredCards.cards.add(cardToCardBody(card))
       //}

       //return filteredCards

        //var allCards = cardRepository.findAll()
        //var allCardHouses: List<CardHouses>? = null
        //var allCardKeywords: List<CardKeywords>? = null
        //var allCardTraits: List<CardTraits>? = null
        //for (card in allCards) {
        //    if (name != "_NONAME" && card.name != name)
        //        continue
//
        //    if (text != "_NOTEXT" && !card.text.contains(text, true))
        //        continue
//
        //    if (aember != "_NOAEMBER" && card.aember != aember)
        //        continue
//
        //    if (power != "_NOPOWER" && card.power != power)
        //        continue
//
        //    if (armor != "_NOARMOR" && card.armor != armor)
        //        continue
//
        //    if (artist != "_NOARTIST" && card.artist != artist)
        //        continue
//
        //    if(types != null)
        //        if (!types.contains(card.type.name))
        //            continue
//
        //    if (rarities != null)
        //        if (!rarities.contains(card.rarity.name))
        //            continue
//
        //    if (keywords != null) {
        //        var keywordsGood = true
        //        //if (allCardKeywords == null)
        //        val allCardKeywords = cardKeywordsRepository.findByCardId(card.id!!)
        //        for (keyword in allCardKeywords) {
        //            if (!keywords.contains(keyword.keyword.name)) {
        //                keywordsGood = false
        //                break
        //            }
        //        }
//
        //        if (!keywordsGood)
        //            continue
        //    }
//
        //    if (houses != null) {
        //        var housesGood = true
        //        //if (allCardHouses == null)
        //        val allCardHouses = cardHousesRepository.findByCardId(card.id!!)
        //        for (house in allCardHouses) {
        //            if (!houses.contains(house.house.name)) {
        //                housesGood = false
        //                break
        //            }
        //        }
//
        //        if (!housesGood)
        //            continue
        //    }
//
        //    if (traits != null) {
        //        var traitsGood = true
        //        //if (allCardTraits == null)
        //        val allCardTraits = cardTraitsRepository.findByCardId(card.id!!)
        //        for (trait in allCardTraits) {
        //            if (!traits.contains(trait.trait.name)) {
        //                traitsGood = false
        //                break
        //            }
        //        }
//
        //        if (!traitsGood)
        //            continue
        //    }
//
        //    filteredCards.cards.add(cardToCardBody(card))
        //}

        //return filteredCards
    }

    fun slugify(word: String, replacement: String = "-") = Normalizer
            .normalize(word, Normalizer.Form.NFD)
            .replace("[^\\p{ASCII}]".toRegex(), "")
            .replace("[^a-zA-Z0-9\\s]+".toRegex(), "").trim()
            .replace("\\s+".toRegex(), replacement)
            .toLowerCase()
}