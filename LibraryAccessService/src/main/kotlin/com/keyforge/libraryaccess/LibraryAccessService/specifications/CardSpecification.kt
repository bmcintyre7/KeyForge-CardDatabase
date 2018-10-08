package com.keyforge.libraryaccess.LibraryAccessService.specifications

import au.com.console.jpaspecificationdsl.*
import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import com.keyforge.libraryaccess.LibraryAccessService.responses.RarityBody
import org.springframework.data.jpa.domain.Specifications

data class CardQuery(
    val name: String? = null,
    val text: String? = null,
    val types: MutableList<String>? = null,
    val houses: MutableList<String>? = null,
    val keywords: MutableList<String>? = null,
    val rarities: MutableList<String>? = null,
    val aember: String? = null,
    val power: String? = null,
    val armor: String? = null,
    val artist: String? = null
)

fun hasName(name: String?): Specifications<Card>? = name?.let {
    Card::name.equal(it)
}

fun hasText(text: String?): Specifications<Card>? = text?.let {
    Card::text.like("%" + text + "%")
}

fun hasTypeIn(types: MutableList<String>?): Specifications<Card>? = types?.let {
    or ( types.map { hasType(it) })
}

fun hasType(type: String?): Specifications<Card>? = type?.let {
    Card::type.equal(Type(name = it))
}

fun hasRarityIn(rarities: MutableList<RarityBody>?): Specifications<Card>? = rarities?.let {
    or ( rarities.map { hasRarity(it) })
}

fun hasRarity(rarity: RarityBody?): Specifications<Card>? = rarity?.let {
    Card::rarity.equal(it)
}

//fun hasHouseIn(houses: MutableList<String>?): Specifications<Card>? = houses?.let {
//    or ( houses.map { hasHouse(it) })
//}
//
//fun hasHouse(house: String?): Specifications<Card>? = house?.let {
//    Card::house.equal(Rarity(name = it))
//}


//fun hasKeywords(keywords: MutableList<String>?): Specifications<Card>? = keywords?.let {
//    and ( keywords.map { hasKeyword(it) })
//}
//
//fun hasKeyword(keyword: String?): Specifications<Card>? = keyword?.let {
//    Card::type.name.
//}

fun CardQuery.toSpecification() : Specifications<Card> = and(
    hasName(name),
    hasText(text),
    hasTypeIn(types),
    //hasKeywords(keywords),
    hasRarityIn(rarities)
    //hasHouseIn(houses)
    //hasAember(aember),
    //hasPower(power),
    //hasArmor(armor),
    //hasArtist(artist)
)