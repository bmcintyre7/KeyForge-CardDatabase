package com.keyforge.libraryaccess.LibraryAccessService.specifications

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.data.Rarity
import org.springframework.data.jpa.domain.Specifications

data class CardQuery(
    val name: String? = null,
    val text: String? = null,
    val types: MutableList<String>? = null,
    val houses: MutableList<House>? = null,
    val keywords: MutableList<String>? = null,
    val rarities: MutableList<Rarity>? = null,
    val aember: String? = null,
    val power: String? = null,
    val armor: String? = null,
    val artist: String? = null
) {
    fun hasName(name: String?): Specifications<Card>? = name?.let {
        Card::name.likeIgnoreCase(it)
    }

    fun hasText(text: String?): Specifications<Card>? = text?.let {
        Card::text.likeIgnoreCase("%" + text + "%")
    }

    fun hasTypeIn(types: List<String>?): Specifications<Card>? = types?.let {
        Card::type.`in`(types)
    }

    fun hasRarityIn(rarities: List<Rarity>?): Specifications<Card>? = rarities?.let {
        Card::rarity.`in`(rarities)
        //or ( rarities.map( ::hasRarity ) )
    }

    fun hasArtist(artist: String?): Specifications<Card>? = artist?.let {
        Card::artist.likeIgnoreCase(artist)
    }

    fun toSpecification() : Specifications<Card> = and(
        hasName(name),
        hasText(text),
        hasTypeIn(types),
        //hasKeywords(keywords),
        hasRarityIn(rarities),
        //hasHouseIn(houses)
        //hasAember(aember),
        //hasPower(power),
        //hasArmor(armor),
        hasArtist(artist)
    )
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

