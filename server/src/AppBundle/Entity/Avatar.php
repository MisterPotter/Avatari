<?php
// src/Appbundle/Entity/Avatar.php
namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Entity
* @ORM\Table(name="avatar")
*/
class Avatar
{
    /**
    * @ORM\Column(type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=100)
     */
    private $name;

    /**
     * @ORM\OneToOne(targetEntity="Account", inversedBy="avatar")
     */
     private $account;

     /**
     * @ORM\ManyToOne(targetEntity="Tari")
     */
     private $tari;

     /**
     * @ORM\ManyToOne(targetEntity="Area")
     */
     private $area;

     /**
      * @ORM\OneToMany(targetEntity="Goal", mappedBy="avatar")
      */
      private $goals;

      /**
       * @ORM\ManyToMany(targetEntity="Item", mappedBy="avatars")
       */
       private $items;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $head;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $body;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $feet;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $hands;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $wings;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $neck;

       /**
       * @ORM\ManyToOne(targetEntity="Item")
       */
       private $ring;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $level;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $exp_max;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $exp_current;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $health_max;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $health_current;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $agility_base;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $defence_base;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $strength_base;



    /**
     * Get id
     *
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set name
     *
     * @param string $name
     *
     * @return Avatar
     */
    public function setName($name)
    {
        $this->name = $name;

        return $this;
    }

    /**
     * Get name
     *
     * @return string
     */
    public function getName()
    {
        return $this->name;
    }
    /**
     * Constructor
     */
    public function __construct()
    {
        $this->goals = new \Doctrine\Common\Collections\ArrayCollection();
        $this->items = new \Doctrine\Common\Collections\ArrayCollection();
        $this->level = 1;
        $this->exp_max = 100;
        $this->exp_current = 0;
        $this->health_max = 100;
        $this->health_current = 100;
        $this->agility_base = 5;
        $this->defence_base = 5;
        $this->strength_base = 5;
        $this->area = 1;
        $this->tari = 1;
    }

    /**
     * Set account
     *
     * @param \AppBundle\Entity\Account $account
     *
     * @return Avatar
     */
    public function setAccount(\AppBundle\Entity\Account $account = null)
    {
        $this->account = $account;

        return $this;
    }

    /**
     * Get account
     *
     * @return \AppBundle\Entity\Account
     */
    public function getAccount()
    {
        return $this->account;
    }

    /**
     * Add goal
     *
     * @param \AppBundle\Entity\Goal $goal
     *
     * @return Avatar
     */
    public function addGoal(\AppBundle\Entity\Goal $goal)
    {
        $this->goals[] = $goal;

        return $this;
    }

    /**
     * Remove goal
     *
     * @param \AppBundle\Entity\Goal $goal
     */
    public function removeGoal(\AppBundle\Entity\Goal $goal)
    {
        $this->goals->removeElement($goal);
    }

    /**
     * Get goals
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getGoals()
    {
        return $this->goals;
    }

    /**
     * Add item
     *
     * @param \AppBundle\Entity\Item $item
     *
     * @return Avatar
     */
    public function addItem(\AppBundle\Entity\Item $item)
    {
        $this->items[] = $item;

        return $this;
    }

    /**
     * Remove item
     *
     * @param \AppBundle\Entity\Item $item
     */
    public function removeItem(\AppBundle\Entity\Item $item)
    {
        $this->items->removeElement($item);
    }

    /**
     * Get items
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getItems()
    {
        return $this->items;
    }

    /**
     * Set level
     *
     * @param integer $level
     *
     * @return Avatar
     */
    public function setLevel($level)
    {
        $this->level = $level;

        return $this;
    }

    /**
     * Get level
     *
     * @return \int
     */
    public function getLevel()
    {
        return $this->level;
    }

    /**
     * Set expMax
     *
     * @param integer $expMax
     *
     * @return Avatar
     */
    public function setExpMax($expMax)
    {
        $this->exp_max = $expMax;

        return $this;
    }

    /**
     * Get expMax
     *
     * @return \int
     */
    public function getExpMax()
    {
        return $this->exp_max;
    }

    /**
     * Set expCurrent
     *
     * @param integer $expCurrent
     *
     * @return Avatar
     */
    public function setExpCurrent($expCurrent)
    {
        $this->exp_current = $expCurrent;

        return $this;
    }

    /**
     * Get expCurrent
     *
     * @return \int
     */
    public function getExpCurrent()
    {
        return $this->exp_current;
    }

    /**
     * Set healthMax
     *
     * @param integer $healthMax
     *
     * @return Avatar
     */
    public function setHealthMax($healthMax)
    {
        $this->health_max = $healthMax;

        return $this;
    }

    /**
     * Get healthMax
     *
     * @return \int
     */
    public function getHealthMax()
    {
        return $this->health_max;
    }

    /**
     * Set healthCurrent
     *
     * @param integer $healthCurrent
     *
     * @return Avatar
     */
    public function setHealthCurrent($healthCurrent)
    {
        $this->health_current = $healthCurrent;

        return $this;
    }

    /**
     * Get healthCurrent
     *
     * @return \int
     */
    public function getHealthCurrent()
    {
        return $this->health_current;
    }

    /**
     * Set agilityBase
     *
     * @param integer $agilityBase
     *
     * @return Avatar
     */
    public function setAgilityBase($agilityBase)
    {
        $this->agility_base = $agilityBase;

        return $this;
    }

    /**
     * Get agilityBase
     *
     * @return integer
     */
    public function getAgilityBase()
    {
        return $this->agility_base;
    }

    /**
     * Set defenceBase
     *
     * @param integer $defenceBase
     *
     * @return Avatar
     */
    public function setDefenceBase($defenceBase)
    {
        $this->defence_base = $defenceBase;

        return $this;
    }

    /**
     * Get defenceBase
     *
     * @return integer
     */
    public function getDefenceBase()
    {
        return $this->defence_base;
    }

    /**
     * Set strengthBase
     *
     * @param integer $strengthBase
     *
     * @return Avatar
     */
    public function setStrengthBase($strengthBase)
    {
        $this->strength_base = $strengthBase;

        return $this;
    }

    /**
     * Get strengthBase
     *
     * @return integer
     */
    public function getStrengthBase()
    {
        return $this->strength_base;
    }


    /**
     * Set head
     *
     * @param \AppBundle\Entity\Item $head
     *
     * @return Avatar
     */
    public function setHead(\AppBundle\Entity\Item $head = null)
    {
        $this->head = $head;

        return $this;
    }

    /**
     * Get head
     *
     * @return \AppBundle\Entity\Item
     */
    public function getHead()
    {
        return $this->head;
    }

    /**
     * Set body
     *
     * @param \AppBundle\Entity\Item $body
     *
     * @return Avatar
     */
    public function setBody(\AppBundle\Entity\Item $body = null)
    {
        $this->body = $body;

        return $this;
    }

    /**
     * Get body
     *
     * @return \AppBundle\Entity\Item
     */
    public function getBody()
    {
        return $this->body;
    }

    /**
     * Set feet
     *
     * @param \AppBundle\Entity\Item $feet
     *
     * @return Avatar
     */
    public function setFeet(\AppBundle\Entity\Item $feet = null)
    {
        $this->feet = $feet;

        return $this;
    }

    /**
     * Get feet
     *
     * @return \AppBundle\Entity\Item
     */
    public function getFeet()
    {
        return $this->feet;
    }

    /**
     * Set hands
     *
     * @param \AppBundle\Entity\Item $hands
     *
     * @return Avatar
     */
    public function setHands(\AppBundle\Entity\Item $hands = null)
    {
        $this->hands = $hands;

        return $this;
    }

    /**
     * Get hands
     *
     * @return \AppBundle\Entity\Item
     */
    public function getHands()
    {
        return $this->hands;
    }

    /**
     * Set wings
     *
     * @param \AppBundle\Entity\Item $wings
     *
     * @return Avatar
     */
    public function setWings(\AppBundle\Entity\Item $wings = null)
    {
        $this->wings = $wings;

        return $this;
    }

    /**
     * Get wings
     *
     * @return \AppBundle\Entity\Item
     */
    public function getWings()
    {
        return $this->wings;
    }

    /**
     * Set neck
     *
     * @param \AppBundle\Entity\Item $neck
     *
     * @return Avatar
     */
    public function setNeck(\AppBundle\Entity\Item $neck = null)
    {
        $this->neck = $neck;

        return $this;
    }

    /**
     * Get neck
     *
     * @return \AppBundle\Entity\Item
     */
    public function getNeck()
    {
        return $this->neck;
    }

    /**
     * Set ring
     *
     * @param \AppBundle\Entity\Item $ring
     *
     * @return Avatar
     */
    public function setRing(\AppBundle\Entity\Item $ring = null)
    {
        $this->ring = $ring;

        return $this;
    }

    /**
     * Get ring
     *
     * @return \AppBundle\Entity\Item
     */
    public function getRing()
    {
        return $this->ring;
    }

    /**
     * Set tari
     *
     * @param \AppBundle\Entity\Tari $tari
     *
     * @return Avatar
     */
    public function setTari(\AppBundle\Entity\Tari $tari = null)
    {
        $this->tari = $tari;

        return $this;
    }

    /**
     * Get tari
     *
     * @return \AppBundle\Entity\Tari
     */
    public function getTari()
    {
        return $this->tari;
    }

    /**
     * Set area
     *
     * @param \AppBundle\Entity\Area $area
     *
     * @return Avatar
     */
    public function setArea(\AppBundle\Entity\Area $area = null)
    {
        $this->area = $area;

        return $this;
    }

    /**
     * Get area
     *
     * @return \AppBundle\Entity\Area
     */
    public function getArea()
    {
        return $this->area;
    }
}
